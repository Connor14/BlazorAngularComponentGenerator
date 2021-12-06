import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  greetingText = 'Welcome to angular-app-with-blazor!';

  blazorCounters: any[] = [];
  nextCounterIndex = 1;

  addBlazorCounter() {
    const index = this.nextCounterIndex++;
    this.blazorCounters.push({
      title: `Counter ${index}`,
      incrementAmount: index,
      customObject: { StringValue: 'Hello!', IntegerValue: 42 }
    });
  }

  removeBlazorCounter() {
    this.blazorCounters.pop();
  }

  modifyParameters() {
    for (const counter of this.blazorCounters) {
      counter.incrementAmount++;

      let { StringValue, IntegerValue } = counter.customObject;

      StringValue += '!';
      IntegerValue -= 1;

      counter.customObject = { StringValue, IntegerValue };
    }
  }

  customCallback(mouseEventArgs: any) {
    console.log(`Custom callback: ${JSON.stringify(mouseEventArgs)}`);
  }
}
