'use strict';

import './index.html';

import 'bootstrap/dist/css/bootstrap.min.css';

import 'bootstrap'

import { Elm } from './elm/Main.elm';

Elm.Main.init({
    node: document.getElementById('main')
});