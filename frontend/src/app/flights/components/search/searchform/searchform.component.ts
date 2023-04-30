import { ChangeDetectorRef, Component, Input, OnInit, ViewChild, ViewEncapsulation, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { ApiService } from 'src/app/flights/services/api.service';
import { SearchresultsComponent } from '../searchresults/searchresults.component';
import { FlightService } from 'src/app/flights/services/flight.service';

@Component({
  selector: 'app-searchform',
  templateUrl: './searchform.component.html',
  styleUrls: ['./searchform.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SearchformComponent implements OnInit{
@ViewChild(SearchresultsComponent) childResults!: SearchresultsComponent;
@Input() searched: boolean = false;
  
isOneWay: boolean = false;
 
from: string = '';
destination: string = '';
fromDate: string = null!;
toDate: string = null!;

getCities$: Observable<string[]> = new Observable<string[]>;
inputUpdate = new Subject<string>();

private apiService = inject(ApiService);
private flightService = inject(FlightService);

minDate = new Date("4/15/2023");
maxDate = new Date("4/20/2023");

form = new FormGroup (
{
  'from': new FormControl(localStorage.getItem('from')!, [Validators.required,     Validators.minLength(3)]),    
  'destination': new FormControl(localStorage.getItem('destination')!, 
    [Validators.required, Validators.minLength(3)]),
  'fromDate': new FormControl('', Validators.required),  
  'toDate': new FormControl('')
});

constructor(private router: Router, private cdr: ChangeDetectorRef){}

return() {
  if (this.isOneWay === false) {
    this.flightService.isOneWay = true;
    this.isOneWay = true;
    this.cdr.detectChanges();
  } 
  if (this.isOneWay === true) { 
    this.flightService.isOneWay = false;
    this.isOneWay = false;
    this.cdr.detectChanges(); 
  }
}

ngOnInit(){
  this.inputUpdate.pipe(debounceTime(800),distinctUntilChanged())
      .subscribe((v) => {
        if(v.length >= 1){
          this.getCities$ = this.apiService.getCities(v)
        }}
  );
    
 var fromDate_ = localStorage.getItem('fromDate')!;
 if(fromDate_ != null){
    this.fromDate = new Date(fromDate_).toDateString().split(' ').slice(1).join(' ');
 }
 var toDate_ = localStorage.getItem('toDate')!;
  if(toDate_ != null){
    this.toDate = new Date(toDate_).toDateString().split(' ').slice(1).join(' ');
  }
}
    
getFlights(){
  localStorage.setItem('from',this.form.value.from!);
  localStorage.setItem('destination',this.form.value.destination!);
  localStorage.setItem('fromDate', (this.form.value.fromDate!));
  localStorage.setItem('toDate',this.form.value.toDate!);

  if(this.searched == false){
    this.router.navigate(['/search']);
  }
  else{
    this.childResults.callFlights();   
  }  
}

}
