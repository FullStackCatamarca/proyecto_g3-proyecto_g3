import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';

@Component({
  selector: 'app-header-forum',
  templateUrl: './header-forum.component.html',
  styleUrls: ['./header-forum.component.css']
})


export class HeaderForumComponent implements OnInit {


  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    }

}
