<template>
	<v-container class="h-100">
		<div>TEST CHAR PAGE!</div>
	</v-container>
</template>

<script setup>
import { useStarWarsPeople } from '@/stores/starwars-people.store';

const route = useRoute();
const starWarsCharacters = useStarWarsPeople();

onMounted(() => {
	const id = Number(route.params.id);
	if (isNaN(id)) throw new Error('id is not a number');
	starWarsCharacters.fetchPeopleDetail(id);
});
</script>

<style lang="scss" scoped></style>

