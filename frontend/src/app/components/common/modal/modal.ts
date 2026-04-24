import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  imports: [CommonModule],
  templateUrl: './modal.html',
})
export class Modal {
  @Input() isOpen = false;
  @Input() title = '';
  @Input() confirmText = 'Confirm';
  @Input() cancelText = 'Cancel';
  @Output() closed = new EventEmitter<void>();
  @Output() confirmed = new EventEmitter<void>();

  confirm() {
    this.confirmed.emit();
    this.close();
  }
  close() {
    this.closed.emit();
  }
}
