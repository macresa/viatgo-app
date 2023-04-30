import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlightsRoutingModule } from './flights-routing.module';
import { HomeComponent } from './components/home/home.component';
import { CardsComponent } from './components/cards/cards.component';
import { SearchComponent } from './components/search/search.component';
import { SearchformComponent } from './components/search/searchform/searchform.component';
import { SearchresultsComponent } from './components/search/searchresults/searchresults.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { NgPipesModule } from 'ngx-pipes';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { MyflightComponent } from './components/myflight/myflight.component';
@NgModule({
  declarations: [
    HomeComponent,
    CardsComponent,
    SearchComponent,
    SearchformComponent,
    SearchresultsComponent,
    CheckoutComponent,
    MyflightComponent
  ],
  imports: [
    CommonModule,
    FlightsRoutingModule,
    MatRadioModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgPipesModule
  ]
})
export class FlightsModule { }
