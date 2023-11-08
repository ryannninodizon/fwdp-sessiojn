import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ItemComponent } from './item/item.component';
import { ItemService } from './item.service';

const appRoutes: Routes = [
  { path: 'items', component: ItemComponent },
  { path: '', redirectTo: '/items', pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    ItemComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [ItemService],
  bootstrap: [AppComponent]
})
export class AppModule { }