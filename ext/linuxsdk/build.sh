#!/bin/bash -ex

export sdk=$(dirname $(readlink -f $0))
export ext=$(dirname $sdk)

# build crosstool-ng if not already built
if [ ! -d $sdk/crosstool-ng/bin ]
then
	pushd $ext/crosstool-ng
	./bootstrap
	./configure --prefix=$sdk/crosstool-ng
	make install
	popd
fi
export PATH=$sdk/crosstool-ng/bin:$PATH

# if not passed a target, recurse into available targets
target=$1
if [ -z "$target" ]
then
	for i in $(ls *.dir/.config | cut -d'.' -f1);
	do
		$0 $i
	done
	exit
fi

# common directories
dist=$sdk/$target
home=$sdk/$target.dir
root=$home/root

# build cross compiler
if [ ! -f $home/stamp ]
then
	mkdir -p /tmp/ctngsrc
	pushd $home
	ct-ng upgradeconfig
	ct-ng build
	touch stamp
	popd
fi

# use tools from root
export PATH=$root/bin:$PATH

# install dist kernel headers
# translate target into kernel arch name
case "${target%%-*}" in
	x86_64) kernel_arch="x86";;
	i686) kernel_arch="x86";;
	arm*) kernel_arch="arm";;
	aarch64) kernel_arch="arm64";;
esac

# copy Linux headers for distribution
if [ ! -f $dist.linux/stamp ]
then
	pushd $ext/linux
	make headers_install ARCH=$kernel_arch INSTALL_HDR_PATH=$dist.linux
	popd
	pushd $dist.linux
	mkdir -p $dist/include
	cp -rv include/* $dist/include
	popd
	touch stamp
fi

# build GLIBC for distribution
if [ ! -f $dist.glibc/stamp ]
then
	mkdir -p $dist.glibc
	pushd $dist.glibc
	$ext/glibc/configure \
		--host=$target \
		--target=$target \
		--prefix="" \
		--with-sysroot=$root \
		--with-headers=$dist/include \
		--disable-nls --disable-multilib --disable-selinux --disable-profile --disable-tunables
	make
	make DESTDIR=$dist install
	touch stamp
	popd
fi

# build GCC for distribution
if [ ! -f $dist.gcc/stamp ]
then
	# rely on built in versions of libraries
	pushd $ext/gcc
	./contrib/download_prerequisites
	popd

	mkdir -p $dist.gcc
	pushd $dist.gcc
	$ext/gcc/configure \
		--host=$target \
		--target=$target \
		--prefix="" \
		--with-sysroot=$dist \
		--disable-bootstrap --disable-nls --disable-multilib --enable-languages=c,c++
	make all-gcc all-target-libgcc all-target-libstdc++
	make DESTDIR=$dist install-target-libgcc install-target-libstdc++
	touch stamp
	popd
fi