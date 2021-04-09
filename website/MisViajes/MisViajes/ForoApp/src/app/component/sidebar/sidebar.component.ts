import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  @Output() sidebarEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  fijarSelector(tipo: string) : void
  {
    this.sidebarEvent.emit(tipo);
  }

}
