import { Component, OnInit, Input } from '@angular/core';
import { TemasService } from '../../temas.service';
import { Temas } from '../../temas';
import { Posts } from '../../posts';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  valor:Temas[] = [];
  
  constructor(private temasService: TemasService) { }

  ngOnInit(): void {
    this.temasService.sendGetRequest("0").subscribe(data => {
      this.valor = data;
    })

  }

 ordenarPor(tipo: string): void {
    this.temasService.sendGetRequest(tipo).subscribe(data => {
      this.valor = data;
    })
  }
  Buscar(frase: string): void {
    this.temasService.sendFindRequest(frase).subscribe(data => {
      this.valor = data;
    })
  }
}
