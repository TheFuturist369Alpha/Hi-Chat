import { Component, Input, OnInit } from '@angular/core';
import { Message } from '../Models/Message';
import { ChatServiceService } from '../Services/chat-service.service';

@Component({
  selector: 'app-message-comp',
  templateUrl: './message-comp.component.html',
  styleUrls: ['./message-comp.component.css']
})
export class MessageCompComponent implements OnInit {
   @Input() messages:Message[]=[];

  constructor(public _client:ChatServiceService) { }

  ngOnInit(): void {
  }

}
