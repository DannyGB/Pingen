import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-pin-list',
  templateUrl: './pin-list.component.html',
  styleUrls: ['./pin-list.component.css']
})
export class PinListComponent implements OnInit {

  constructor(private http: HttpClient) { }

  pinNumbers:any = [];

  ngOnInit(): void {
    this.start();
  }

  start() {
    setInterval(() => {
      this.getPins();
    }, 5000);
  }

  getPins() {
    const url = "http://localhost:7071/api/pin";
    this.http.get(url).subscribe((response: any) => {    
        this.pinNumbers.unshift(`Pin ${response.Pin} generated ${(new Date()).toUTCString()}`);
    });
  };
}
