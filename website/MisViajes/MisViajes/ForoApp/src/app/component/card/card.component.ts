import { Component, OnInit, Input } from '@angular/core';
import { TemasService } from '../../temas.service';
import { Temas } from '../../temas';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  valor:Temas[] = [];

  constructor(private temasService: TemasService) { }

  // constructor() { }

  ngOnInit(): void {
    this.temasService.sendGetRequest().subscribe(data => {
      console.log(data);
      this.valor = data;
    })
  }

}
