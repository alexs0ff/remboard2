import { Component, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Router } from '@angular/router'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  response$: Observable<string>;

  constructor(private httpClient: HttpClient, private router: Router) {}


  send() {
    //this.response$ = this.httpClient.delete<string>("api/AutocompleteItem/A878952B-8D87-4D04-B2D2-480FD60D003E");
    //this.response$ = this.httpClient.put<string>("api/AutocompleteItem/A878952B-8D87-4D04-B2D2-480FD60D003E",{ Id: "A878952B-8D87-4D04-B2D2-480FD60D003E", Title: "!Test modified", AutocompleteKindId:1 });
    //this.response$ = this.httpClient.post<string>("api/AutocompleteItem",{ Id: "A878952B-8D87-4D04-B2D2-480FD60D003E", Title: "Test", AutocompleteKindId:1 });
    //this.response$ = this.httpClient.get<string>("api/AutocompleteItem/A31CCB6A-8B28-4B96-A278-E3CF9DF5E130");
    //this.response$ = this.httpClient.get<string>("api/AutocompleteItems?filterColumnName=Title&filterColumnTitleValue=t&filterColumnTitleOperator=Contains&orderby=title&orderkind=desc");
    //this.response$ = this.httpClient.get<string>("api/AutocompleteItems");
    this.response$ = this.httpClient.get<string>("api/AutocompleteKinds");
  }

  test() {
    this.router.navigateByUrl("orders/autocomplete-item/sdsdsdsdsdsd");
  }
}
