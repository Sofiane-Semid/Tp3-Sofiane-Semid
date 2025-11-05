import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModule } from '../material.module';
import { FormsModule } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { GameMobileService } from '../services/game-mobile.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MaterialModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  hide = true;

  registerUsername : string = "";
  registerEmail : string = "";
  registerPassword : string = "";
  registerPasswordConfirm : string = "";

  loginUsername : string = "";
  loginPassword : string = "";

  constructor(public route : Router, public game : GameMobileService ) { }

  ngOnInit() {
  }

  async login(){

    try{
      const l = await this.game.login(this.loginUsername, this.loginPassword)
      console.log(l)
      this.route.navigate(["/play"]);
    }
    catch(ereur){
      console.error(ereur)
      alert('faux')

    }

    // Redirection si la connexion a réussi :
    
  }

  async register(){
    try{
      const r = await this.game.register(this.registerUsername, this.registerEmail,this.registerPassword, this.registerPasswordConfirm)
      console.log(r)
      
    }
    catch(ereur){
      console.error(ereur)
      alert('faux')

    }
    

    
    


  }
  
}
