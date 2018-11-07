import React, { Component } from 'react';
import { Blockchain } from './Blockchain.js';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <Blockchain/>
    );
  }
}
