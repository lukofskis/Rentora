const HeaderBanner = () => {
    return (
        <section
            className="relative w-full h-[25vh] md:h-[30vh] bg-cover bg-center bg-gradient-to-r from-blue-500 to-purple-600"
        >
            <div className="absolute inset-0 opacity-50"></div>

            <div className="relative z-10 flex flex-col items-center justify-center w-full h-full text-center text-white px-4">
                <div className="space-y-6">
                    <h1 className="text-4xl sm:text-3xl lg:text-6xl font-extrabold leading-tight mb-4">
                        Welcome to Rentora
                    </h1>
                </div>

                <div className="mt-4">
                    <img
                        src="/src/assets/foto.png"
                        alt="Home rental illustration"
                        className="w-[40%] max-w-xs mx-auto rounded-lg shadow-lg"
                        style={{ maxHeight: "150px", objectFit: "contain" }}
                    />
                </div>
            </div>
        </section>
    );
};

export default HeaderBanner;
