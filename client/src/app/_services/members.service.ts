import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { Member } from '../Models/Member';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers() {

    return this.http.get<Member[]>(this.baseUrl + 'appusers');
  }

  getMember(username: string) {
    
    return this.http.get<Member>(this.baseUrl + 'appusers/' + username);
  }
}


