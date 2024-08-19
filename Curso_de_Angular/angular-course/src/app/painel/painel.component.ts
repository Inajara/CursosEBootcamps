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
    public rodadaFrase!: Frase;
    public progresso: number = 0

    constructor(){
        this.atualizaRodada()
    }

    public atualizaResposta(resposta: Event): void{
        this.resposta = (<HTMLInputElement>resposta.target).value
    }

    public verificaResposta(): void{
        if (this.rodadaFrase.frasePt == this.resposta) {
            alert("Resposta certa")
            this.rodada++
            this.progresso += (100/this.frases.length)
            this.atualizaRodada()
            this.resposta = ''
        }else{
            alert("Resposta errada")
        }
    }

    public atualizaRodada(): void{
        this.rodadaFrase = this.frases[this.rodada]
    }
}
