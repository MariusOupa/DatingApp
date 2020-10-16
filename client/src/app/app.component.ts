import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'My First App';
  users: any;
  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:44342/appuser').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
}
