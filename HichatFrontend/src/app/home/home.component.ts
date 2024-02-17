import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChatServiceService } from '../Services/chat-service.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  form:FormGroup=new FormGroup({});
  isSubmited:boolean=false;
  nameField:string="";
  errorLogs:string[]=[];
  open_chat:boolean=false;

  constructor(private builder:FormBuilder, private _client:ChatServiceService) { }

  ngOnInit(): void {
    this.onLoad();

  }

  private onLoad():void{
    this.form=this.builder.group({name:[this.nameField, [Validators.required,
    Validators.minLength(3), Validators.maxLength(9)]]});
  }

  public onSubmit():void{
   this.isSubmited=true;
   if(this.form.valid){
    this._client.RegisterUser(this.form.value).subscribe({
      next:()=>{
        console.log("Welcome to Hi Chat");
        this._client.currentUser=this.form.get("name")?.value;
        this.open_chat=true;
        this.isSubmited=false;
        this.form.reset();
        this.errorLogs=[];

      },

      error:(error:any)=>{
        if(typeof(error.error)!="object"){
         this.errorLogs.push(error.error);
        }
      },
      complete:()=>{

      }
    });



   }
    else
    console.log("Form not valid");

  }
  public CloseChat():void{
    this.open_chat=false;
  }

}
