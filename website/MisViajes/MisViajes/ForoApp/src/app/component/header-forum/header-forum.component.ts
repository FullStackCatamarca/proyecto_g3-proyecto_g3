import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DataService } from '../../data.service';

@Component({
  selector: 'app-header-forum',
  templateUrl: './header-forum.component.html',
  styleUrls: ['./header-forum.component.css']
})


export class HeaderForumComponent implements OnInit {

  @Output() HeaderEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
    }

  buscar(frase: string): void {
    this.HeaderEvent.emit(frase);
  }

}
