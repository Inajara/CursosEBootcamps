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

    public atualizaResposta(resposta: Event): void{
        this.resposta = (<HTMLInputElement>resposta.target).value
    }
}
