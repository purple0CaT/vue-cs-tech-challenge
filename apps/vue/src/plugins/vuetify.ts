/**
 * plugins/vuetify.ts
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Composables
import { createVuetify, type ThemeDefinition } from 'vuetify';
// Styles
import '@mdi/font/css/materialdesignicons.css';

import 'vuetify/styles';

const base: ThemeDefinition = {
	dark: false,
	colors: {
		primary: '#417881ff',
		error: '#ff5252',
		action: '#eca378',
		pending: '#B0BEC5',
		requested: '#699cf0',
		background: '#e6eeee',
	},
};

export default createVuetify({
	theme: {
		variations: false,
		defaultTheme: 'base',
		themes: {
			base,
		},
	},
	defaults: {
		VCard: {
			style: { overflow: 'unset' },
		},
		VRating: {
			style: { 'white-space': 'unset' },
		},
	},
});

