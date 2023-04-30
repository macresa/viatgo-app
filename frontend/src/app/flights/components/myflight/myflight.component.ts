import { Component } from '@angular/core';
import { FlightService } from '../../services/flight.service';
import { Flight } from '../../models/flight';
import { CookieService } from 'ngx-cookie-service';
import jwtDecode from 'jwt-decode';

interface Token {
  unique_name: string;
}

@Component({
  selector: 'app-myflight',
  templateUrl: './myflight.component.html',
  styleUrls: ['./myflight.component.css']
})
export class MyflightComponent {
  vueloIda: Flight = null!;
  vueloVuelta: Flight = null!;
  isOneWay: boolean = false;

  token: string = '';
  decoded: Token = null!;
  userName: string = '';

  constructor(private cookieService: CookieService, private flightService: FlightService){
    this.token = cookieService.get('token');
    this.decoded = jwtDecode<Token>(this.token);
    this.userName = this.decoded.unique_name;
  }

  ngOnInit(){
    this.flightService.getIda$.subscribe((v) => this.vueloIda = v);
    this.flightService.getVuelta$.subscribe((v) => this.vueloVuelta = v);
    this.isOneWay = this.flightService.isOneWay;
    }
}
