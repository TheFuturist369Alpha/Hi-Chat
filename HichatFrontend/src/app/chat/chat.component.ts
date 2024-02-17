import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { ChatServiceService } from '../Services/chat-service.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrivateChatComponent } from '../private-chat/private-chat.component';
import { Message } from '../Models/Message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit,OnDestroy {
 @Output() CloseChatEmitter=new EventEmitter();
 public User:string=this._client.currentUser;
  constructor(public _client:ChatServiceService, private modal:NgbModal) {
     }

  ngOnInit(): void {
    this._client.CreateConnection();
  }

  ngOnDestroy(): void {
      this._client.BreakConnection();
  }
public backHome():void{
  this.CloseChatEmitter.emit();
}

public async sendMessage(content: string){
  this._client.sendMessage(content);
}

public async sendPMessage(content: Message){
  
  this._client.sendPrivateMessage(content);
}

public async openChat(toUser: string){
const ref= this.modal.open(PrivateChatComponent);
ref.componentInstance.toUser=toUser;
}

}
