<template>
	<header class="bg-primary d-flex justify-center align-center">
		<router-link
			style="height: 35px; width: 35px"
			to="/">
			<section class="logo-flip">
				<div class="logo-flip__inner">
					<div class="logo-flip-front">
						<v-img
							alt="Empire SVG"
							class="cursor-pointer error"
							cover
							src="/svgs/empire.svg"
							style="height: 35px" />
					</div>
					<div class="logo-flip-back">
						<v-img
							alt="Rebelion SVG"
							class="cursor-pointer"
							src="/svgs/rebelion.svg"
							style="height: 35px" />
					</div>
				</div>
			</section>
		</router-link>
	</header>
</template>

<script setup></script>

<style lang="scss" scoped>
header {
	height: 50px;
	display: flex;
	justify-content: space-between;
	align-items: center;
	padding: 8px;
}

.logo-flip {
	position: absolute;
	width: 35px;
	height: 35px;
	perspective: 500px;
	//
	&__inner {
		position: absolute;
		width: 100%;
		height: 100%;
		transition: transform 1s;
		transform-style: preserve-3d;
	}
	//
	&:hover {
		& .logo-flip__inner {
			transform: rotateY(180deg);
			transition: transform 0.5s;
		}
	}
}
.logo-flip-front,
.logo-flip-back {
	position: absolute;
	height: 100%;
	width: 100%;
	text-align: center;
	backface-visibility: hidden;
}
.logo-flip-back {
	transform: rotateY(180deg);
}
</style>

