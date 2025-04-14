
<template>
    <header class="sticky top-0 z-30 flex h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6">
        <!--Hidden Mobile Menu (when not enough space for sidebar)-->
        <HeaderMobileMenu/>
            
        <!--Light/Dark Mode Toggle-->
        <div class="right-auto ml-auto flex-1 md:grow-0">
            <HeaderModeToggle />
        </div>
        
        <!--Account Menu-->
        <HeaderAccount />
    </header>
</template>