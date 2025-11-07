import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { Game } from '../play/gameLogic/game';
import { GameMobileService } from '../services/game-mobile.service';

@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores : Score[] = [];
  publicScores : Score[] = [];

  constructor(public gameService: GameMobileService) { }

  async ngOnInit() {

    try{
      this.myScores = await this.gameService.GetMyScores();
       this.publicScores = await this.gameService.GetPublicScores()
      console.log(this.myScores)
    }
    catch(erreur){
      console.error("Erreur : ", erreur)
    }
    

  }

  async changeScoreVisibility(score : Score){

    try {
    await this.gameService.ChangeScoreVisibility(score.id)
    score.isPublic = !score.isPublic;

    console.log(this.publicScores);
  } catch (error) {
    console.error(error);
   
    
  }

 this.publicScores = await this.gameService.GetPublicScores()
  }

}
