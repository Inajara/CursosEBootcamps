import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-progresso',
  standalone: true,
  imports: [],
  templateUrl: './progresso.component.html',
  styleUrl: './progresso.component.css'
})
export class ProgressoComponent {
  @Input() public progresso: number = 0

  constructor(){}
}
