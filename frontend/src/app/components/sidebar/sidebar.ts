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
    // Implementa la logica di navigazione qui, ad esempio usando il Router di Angular
    this.router.navigate([path]);
  }
}
