import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { Slides } from '../../slides';

@Component({
  selector: 'app-header-forum',
  templateUrl: './header-forum.component.html',
  styleUrls: ['./header-forum.component.css']
})


export class HeaderForumComponent implements OnInit {

  slides : Slides[] =  [];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.dataService.sendGetRequest().subscribe(data =>  {
      console.log(data);
    })
    }

}
