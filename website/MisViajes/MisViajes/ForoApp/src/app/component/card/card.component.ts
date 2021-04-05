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
  post: Posts[] = [];

  constructor(private temasService: TemasService) { }

  // constructor() { }

  ngOnInit(): void {
    this.temasService.sendGetRequest().subscribe(data => {
      this.valor = data;
    })
  }

}
