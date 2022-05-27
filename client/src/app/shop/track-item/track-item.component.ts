import {Component, Input, OnInit} from '@angular/core';
import {ITrack} from "../../shared/models/track";

@Component({
  selector: 'app-track-item',
  templateUrl: './track-item.component.html',
  styleUrls: ['./track-item.component.scss']
})
export class TrackItemComponent implements OnInit {

  @Input() track : ITrack;
  constructor() { }

  ngOnInit(): void {
  }

}
