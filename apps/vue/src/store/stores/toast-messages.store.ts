import { nanoid } from 'nanoid';

export type ToastMessage = {
	id: string;
	text: string;
	color: 'success' | 'error' | 'warning' | 'info';
	timeout?: number;
	icon?: string;
	iconSize?: number;
	location?: 'top' | 'bottom' | 'left' | 'right' | 'center';
	variant?: 'flat' | 'elevated' | 'tonal' | 'outlined' | 'text' | 'plain';
	closable?: boolean;
	multiLine?: boolean;
	timer?: boolean | string;
};

export const useToastMessagesStore = defineStore('toast-messages', {
	state: (): {
		queue: ToastMessage[];
	} => ({
		queue: [],
	}),
	getters: {
		getQueue: (state) => state.queue,
	},
	actions: {
		addToQueue(message: Omit<ToastMessage, 'id'>): string {
			const toastMessage: ToastMessage = {
				id: nanoid(),
				text: message.text,
				color: message.color,
				timeout: message.timeout || 3000,
				location: message.location || 'bottom',
				variant: message.variant || 'elevated',
				closable: message.closable ?? true,
				multiLine: message.multiLine || false,
				timer: message.timer || false,
				icon: message.icon,
			};

			this.queue.push(toastMessage);
			return toastMessage.id;
		},

		showError(text: string, options: Partial<ToastMessage> = {}): string {
			return this.addToQueue({
				text,
				color: 'error',
				icon: 'mdi-alert',
				...options,
			});
		},

		removeMessage(id: string) {
			this.queue = this.queue.filter((message) => message.id !== id);
		},
	},
});

