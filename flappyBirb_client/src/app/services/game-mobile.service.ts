import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Score } from '../models/score';

const domain = "https://localhost:44378/";

@Injectable({
  providedIn: 'root'
})
export class GameMobileService{

    constructor(public http : HttpClient) { }
  

    async register(username : string, email : string, password : string, passwordConfirm : string){
    let registerDTO = {
      Username : username,
      Email : email,
      Password : password,
      PasswordConfirm : passwordConfirm
    }

    let x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Register", registerDTO));
    console.log(x);
  }

  async login(user : string, pass : string) : Promise<void>{

    let loginDTO =  {
        username : user,
        password : pass
    };

    let x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Login", loginDTO));
    console.log(x);

    // 🔑 Très important de stocker le token quelque part pour pouvoir l'utiliser dans les futures requêtes !
    localStorage.setItem("token", x.token);
    }

    async saveScore(scorevalue : number, timeinseconds : number) : Promise<void>{

        let token = localStorage.getItem("token");
        let httpOptions = {
        headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
        })
    };


        let dto = {
            TimeInSeconds : timeinseconds,
            Scorevalue : scorevalue
        }
        let x = await lastValueFrom(this.http.post<any>(domain + "api/Scores/PostScore", dto, httpOptions));
        console.log(x);
        
    }

    async GetMyScores(): Promise<Score[]>{

      
      let token = localStorage.getItem("token");
      let httpOptions = {
        headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    })
  };

        let s = await lastValueFrom(this.http.get<Score[]>(domain + "api/Scores/GetMyScores", httpOptions))
        console.log(s);
        return s

    }

    
   async GetPublicScores(): Promise<Score[]>{

      
      let token = localStorage.getItem("token");
      let httpOptions = {
        headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    })
  };

        let s = await lastValueFrom(this.http.get<Score[]>(domain + "api/Scores/GetPublicScores", httpOptions))
        console.log(s);
        return s

    }

    async ChangeScoreVisibility(id : number): Promise<void>{

      let token = localStorage.getItem("token");
      let httpOptions = {
        headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    })
  };
      let s = await lastValueFrom(this.http.put<any>(domain + "api/Scores/ChangeScoreVisibility/"+ id,{}, httpOptions ))
      console.log(s);
  

    }

       
    
  




}