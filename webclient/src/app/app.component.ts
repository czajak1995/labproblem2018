import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'lab';

  static isMock() {
    return false;
  }
}

export const BASE_URL: string = "http://192.168.1.83:3002/api/";
