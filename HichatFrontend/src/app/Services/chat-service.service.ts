import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {Model} from '../Models/Model';
import {Message} from '../Models/Message';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrivateChatComponent } from '../private-chat/private-chat.component';

@Injectable({
  providedIn: 'root'
})
export class ChatServiceService {

private _client:HttpClient
public currentUser:string="";
private chatConnect?:HubConnection;
public onliners:string[]=[];
public messages:Message[]=[];
public pMessages:Message[]=[];
public chatInitiated:boolean=false;
  constructor(private client:HttpClient, private modalServe:NgbModal) {
    this._client=client;
   }

   public RegisterUser(user:Model){
    return this._client.post("https://localhost:5032/api/Chat/register",
    user,{responseType:"text"});
   }

   public CreateConnection(){
    this.chatConnect=new HubConnectionBuilder().withUrl("https://localhost:5032/chat")
    .withAutomaticReconnect().build();
    this.chatConnect.start().catch(error=>{
      console.log(error);
    });

    this.chatConnect.on("User Connected.", ()=>{
     this.AddConnectId();
     console.log("Server called here");

    });

    this.chatConnect.on("Online Users",(onliners)=>{
      console.log("from onliner");
      this.onliners=onliners;
    });

    this.chatConnect?.on("New Message", (message: Message)=>{
  this.messages.push(message);
    });

    this.chatConnect?.on("OpenPChat", (message: Message)=>{
      this.pMessages.push(message);
      this.chatInitiated=true;
      const ref=this.modalServe.open(PrivateChatComponent);
      ref.componentInstance.toUser=message.from;

        });

        this.chatConnect?.on("ReceivePChat", (message: Message)=>{
          this.pMessages=[...this.pMessages, message];
            });

            this.chatConnect?.on("ClosePChat", (message: Message)=>{
               this.chatInitiated=false;
               this.modalServe.dismissAll();
               this.pMessages=[];
                });
   }

   public BreakConnection(){
    this.chatConnect?.stop().catch(error=>{
      console.log(error);
    });
   }

   private async AddConnectId(){
    this.chatConnect?.invoke("AddUserConnectionId",this.currentUser)
    .catch(error=>{
      console.log(error+" From serverside");
    });
   }

   public async sendMessage(content: string){
    const message:Message={
      from:this.currentUser,
      message:content
    }
    this.chatConnect?.invoke("RecieveMessage", message).catch(error=>{
      console.log(error);
    });

   }

   public async sendPrivateMessage(message:Message){
     if(!this.chatInitiated){
      this.chatInitiated=true;
      this.chatConnect?.invoke("CreatePrivateChat", message).then(()=>{
        this.pMessages.push(message);
      }).catch(error=>{
        console.log(error);
      })
     }
     else{
      this.chatConnect?.invoke("RecievePrivateChat", message).catch(error=>{
        console.log(error);
      });
     }
   }

   public async closePrivateChat(otherU:string){
    this.chatConnect?.invoke("RemovePrivateChat",this.currentUser, otherU)
    .catch(error=>{
      console.log(error+" From serverside");
    });
   }
}
