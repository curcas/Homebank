module.exports = {
    mode: "production",

    module: {
        rules: [{
            test: /\.html$/,
            exclude: /node_modules/,
            loader: 'file-loader?name=[name].[ext]'
        },
        {
            test: /\.css$/,
            use: ['style-loader', 'css-loader']
        },
        {
            test: /\.elm$/,
            exclude: [/elm-stuff/, /node_modules/],
            loader: "elm-webpack-loader",
            options: {
                optimize: true
            }
        }]
    }
};