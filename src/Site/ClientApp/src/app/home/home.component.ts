import { Component, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  response$: Observable<string>;

  constructor(private httpClient:HttpClient) {}


  send() {
    //this.response$ = this.httpClient.post<string>("api/AutocompleteItem",{ Id: "A31CCB6A-8B28-4B96-A278-E3CF9DF5E131", Title: "Test", AutocompleteKindId:1 });
    this.response$ = this.httpClient.get<string>("api/AutocompleteItem/A31CCB6A-8B28-4B96-A278-E3CF9DF5E130");
  }
}
