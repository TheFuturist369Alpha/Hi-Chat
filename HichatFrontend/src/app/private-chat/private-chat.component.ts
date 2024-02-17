import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ChatServiceService } from '../Services/chat-service.service';
import { Message } from '../Models/Message';

@Component({
  selector: 'app-private-chat',
  templateUrl: './private-chat.component.html',
  styleUrls: ['./private-chat.component.css']
})
export class PrivateChatComponent implements OnInit,OnDestroy {
  @Input() toUser="";
  constructor(public modal:NgbActiveModal, public _client: ChatServiceService) { }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
      this._client.closePrivateChat(this.toUser);
  }

  public sendMessage(message:string){
    let content:Message={
           from:this._client.currentUser,
           message:message,
           to:this.toUser
    }
    this._client.sendPrivateMessage(content);
  }

}
