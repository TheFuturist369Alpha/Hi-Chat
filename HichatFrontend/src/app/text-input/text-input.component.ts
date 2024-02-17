import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit {

  public content:string="";
  @Output() emitter=new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }
 public SubmitMessage():void{
   if(this.content.trim()!=""){
      this.emitter.emit(this.content);
   }
   this.content="";
 }
}
