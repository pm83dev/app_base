import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.html',
  styleUrls: ['./sidebar.scss'],
})
export class Sidebar {
  constructor(private readonly router: Router) {}

  navigateTo(path: string): void {
    this.router.navigate([path]);
  }

  isActive(path: string): boolean {
    // Per la home, path vuoto deve corrispondere a '' o '/'
    if (path === '') {
      return this.router.url === '/' || this.router.url === '';
    }
    return this.router.url.startsWith('/' + path);
  }
}
