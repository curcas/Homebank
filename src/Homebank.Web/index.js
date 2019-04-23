import 'bootstrap';
import './style.scss';
import 'chart.js';
import 'chartjs-plugin-labels';
import 'chartjs-plugin-colorschemes';

var $ = require('jquery');
window.jQuery = $;
window.$ = $;

import 'select2';

import { library, dom } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';

library.add(fas);
dom.watch();