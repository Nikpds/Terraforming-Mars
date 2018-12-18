import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  parseEnum(_enum): Array<any> {
    const map: { id: number; name: string }[] = [];
    for (const n in _enum) {
      if (typeof _enum[n] === 'number') {
        map.push({ id: <any>_enum[n], name: n });
      }
    }
    return map;
  }

  parseEnumName(type, value): string {
    for (const n in type) {
      if (type[n] === value) {
        return n;
      }
    }
    return '';
  }


  // Get distict elements from any array by combining two values of two different properties
  uniqueMultiPropertyArray = function (arrArg, prop, prop2) {
    return arrArg.filter(function (a) {
      const key = a[prop] + ':' + a[prop2];
      if (!this[key]) {
        this[key] = true;
        return true;
      }
    }, Object.create(null));
  };

  uniqueCombinePropertyArray = function (arrArg, prop, prop2, prop3) {
    return arrArg.filter(function (a) {
      const key = `${a[prop]}:${a[prop2]}/${a[prop3]}`;
      if (!this[key]) {
        this[key] = true;
        return true;
      }
    }, Object.create(null));
  };

  // Get distict elements from any array
  uniqueArray = function (arrArg) {
    return arrArg.filter(function (elem, pos, arr) {
      return arr.indexOf(elem) === pos;
    });
  };

  onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
  }
}
