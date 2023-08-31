
const nodeExternals = require('webpack-node-externals');
const CopyPlugin = require("copy-webpack-plugin");
const path = require('path')
const {
    NODE_ENV = 'production',
} = process.env;

module.exports = {
    mode: "production",
    entry: './src/index.ts',
    mode: NODE_ENV,
    target: 'node',
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: 'ts-loader',
                include: [path.resolve(__dirname, 'src')]
            }
        ]
    },
    output: {
        filename: 'server.js',
        path: path.resolve(__dirname, 'dist')
    },
    resolve: {
        extensions: ['.ts', '.js'],
    },
    externals: [ nodeExternals() ],
    plugins: [
        new CopyPlugin({
          patterns: [
            { from: "src/public", to: "public" },
            { 
                from: path.resolve(__dirname, "package.json"), 
                to: path.resolve(__dirname, "dist")
            },
            { 
                from: path.resolve(__dirname, "package-lock.json"), 
                to: path.resolve(__dirname, "dist")
            },
            { 
                from: path.resolve(__dirname, "install.sh"), 
                to: path.resolve(__dirname, "dist")
            },
          ],
        }),
    ],
}