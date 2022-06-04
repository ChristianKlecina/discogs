import { Component, OnInit } from '@angular/core';
import {GenreDialogComponent} from "../genre-dialog/genre-dialog.component";
import {IGenre} from "../../shared/models/genre";
import {IMedium} from "../../shared/models/medium";
import {CartItem} from "../../shared/models/cartitem";
import {Cart} from "../../shared/models/cart";
import {PanelService} from "../panel.service";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  genres: IGenre[]
  mediums: IMedium[]
  cart: Cart[]
  cartItems: CartItem[]
  constructor(private panelService: PanelService, private matDialog: MatDialog) { }

  ngOnInit(): void {
    this.getGenres()
    this.getMediums()
    this.getCart()
  }


  getGenres(){
    this.panelService.getGenres().subscribe(response => {
      this.genres = response
      console.log(this.genres)
    },error => {
      console.log(error)
    })
  }

  getMediums(){
    this.panelService.getMediums().subscribe(response => {
      this.mediums = response
      console.log(this.mediums)
    },error => {
      console.log(error)
    })
  }

  getCart(){
    this.panelService.getCarts().subscribe(response => {
      this.cart = response

      console.log(this.cart)
    },error => {
      console.log(error)
    })
  }

  insertGenre() {
    this.matDialog.open(GenreDialogComponent)
  }

  deleteGenre(genre: any) {
    this.panelService.deleteGenre(genre.id)
  }
}

