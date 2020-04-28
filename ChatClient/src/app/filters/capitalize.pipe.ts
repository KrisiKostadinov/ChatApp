import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'capitalizeFirst'
})
export class CapitalizePipe implements PipeTransform {

  transform(value: string): string {
    let arr = value.split(' ');
    let newArr: string[] = [];
    
    arr.forEach(word => {
      word = word.charAt(0).toUpperCase() + word.slice(1);
      newArr.push(word);
    });

    return newArr.join(' ').toString();
  }
}
