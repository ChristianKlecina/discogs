import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import { AdminComponent } from './admin/admin.component';
import { GenreDialogComponent } from './genre-dialog/genre-dialog.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    AdminComponent,
    GenreDialogComponent
  ],
  imports: [
    CommonModule,
    AdminPanelRoutingModule,
    FormsModule
  ]
})
export class AdminPanelModule { }
