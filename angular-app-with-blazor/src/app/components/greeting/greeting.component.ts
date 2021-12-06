import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BlazorAdapterComponent } from '../blazor-adapter/blazor-adapter.component';

@Component({
  selector: 'greeting',
  template: '',
})

export class GreetingComponent extends BlazorAdapterComponent {
  @Input() text: string | null = null;
}
