import React, { Component } from 'react';
import { Block } from './Block.js';
import { Transaction } from './Transaction.js';

export class Blockchain extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
              <p>Here is the Blockchain</p>
              <Block />
              <Transaction />
            </div>
        );
    }
}