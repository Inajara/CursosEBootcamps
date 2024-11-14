import { Component } from '@angular/core';
import { Tentativa } from '../shared/tentativa.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-tentativas',
  standalone: true,
  imports: [NgFor],
  templateUrl: './tentativas.component.html',
  styleUrl: './tentativas.component.css'
})
export class TentativasComponent {
  public coracaoVazio: string = "/assets/coracao_vazio.png"
  public coracaoCheio: string = "/assets/coracao_cheio.png"

  public tentativas: Tentativa[] = [
    new Tentativa(true),
    new Tentativa(true),
    new Tentativa(true)
  ]

  constructor(){}
}
