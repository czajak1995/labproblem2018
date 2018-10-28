import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'lab';

  static isMock() {
    return true;
  }
}

export const BASE_URL: string = "http://192.168.43.114:3002/api/";
