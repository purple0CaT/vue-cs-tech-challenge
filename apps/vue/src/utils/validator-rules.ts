export const rules = {
	required: (v: string): boolean | string => !!v || 'Required',
	maxLength300: (v: string): boolean | string =>
		!v || v?.length <= 300 || 'Needs to be a maximum of 300 characters',
};

