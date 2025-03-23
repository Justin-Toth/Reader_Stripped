<template>
    <div class="flex min-h-screen w-full flex-col bg-muted/40">
        <div class="flex flex-col sm:gap-4 sm:py-4 sm:pl-14">
            <!--Header-->
            <Header />

            <!-- Sidebar-->
            <Sidebar />

            <!--Page Content-->
            <slot />
        </div>
    </div>
</template>