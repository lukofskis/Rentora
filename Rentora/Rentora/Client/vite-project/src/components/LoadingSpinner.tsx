const LoadingSpinner = () => {
  return (
    <div className="flex justify-center items-center space-x-2">
          <div className="w-16 h-16 border-4 border-t-4 border-gray-200 border-solid rounded-full animate-spin border-t-purple-700"></div>
    </div>
  );
};

export default LoadingSpinner;
