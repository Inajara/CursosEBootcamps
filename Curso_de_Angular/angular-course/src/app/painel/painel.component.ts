import { Component } from '@angular/core';
import { ProgressoComponent } from "../progresso/progresso.component";
import { TentativasComponent } from "../tentativas/tentativas.component";
import { Frase } from '../shared/frase.model';
import { FRASES } from './frase-mock';

@Component({
    selector: 'app-painel',
    standalone: true,
    templateUrl: './painel.component.html',
    styleUrl: './painel.component.css',
    imports: [ProgressoComponent, TentativasComponent]
})
export class PainelComponent {
    public instrucao: string = 'Traduza a frase:'
    public frases: Frase[] = FRASES
    public resposta: string = ''
    public rodada : number = 0
    public rodadaFrase : Frase

    constructor(){
        this.rodadaFrase = this.frases[this.rodada]
    }

    public atualizaResposta(resposta: Event): void{
        this.resposta = (<HTMLInputElement>resposta.target).value
    }

    public verificaResposta(): void{
        this.rodada++
        this.rodadaFrase = this.frases[this.rodada]
    }
}
