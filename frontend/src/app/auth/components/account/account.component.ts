import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/flights/services/api.service';

interface Token {
  unique_name: string;
}

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit{
  token: string = '';
  decoded: Token = null!;
  userName: string = '';

  bookings: any[] = [];

  private apiService = inject(ApiService);

  constructor(private cookieService: CookieService, private router: Router){
    this.token = cookieService.get('token');
    this.decoded = jwtDecode<Token>(this.token);
    this.userName = this.decoded.unique_name;
  }
  ngOnInit(){
    this.getBookings();
  }
  getBookings(){
    this.apiService.getBookings(this.userName).subscribe(
      (v) => this.bookings = v);
  }

  cerrarSesion(){
    setTimeout(() => {
      this.cookieService.set('token','');
      this.router.navigate(['/']);  
    }, 600);
  }

  duration(departure: Date, arrival: Date) {
    const mins = Math.round((new Date(arrival).getTime() - new Date(departure).getTime()) / 60000); 
    const hs = Math.floor(mins / 60);
    const min = mins % 60; 
    return `${hs.toString().padStart(2, '0')}h ${min.toString().padStart(2, '0')}m`; 
  }
}
